#!/bin/bash

#abort on error
set -e

function usage
{
    echo "usage: ./deploy.sh -host host -dbname dbname [-p port || -h]"
    echo "   ";
    echo "  -h | --host             : postgres host ip or name";
    echo "  -n | --dbname           : new database name";
    echo "  -p | --port             : connect port";
    echo "  -h | --help             : This message";
}

function parse_args
{
  # positional args
  args=()

  # named args
  while [ "$1" != "" ]; do
      case "$1" in
          -h | --host )     host="$2";              shift;;
          -n | --dbname )   dbname="$2";            shift;;
          -p | --port )     port="$2";              shift;;
          -h | --help )     usage;                  exit;; # quit and show usage
          * )               args+=("$1")            # if no match, add it to the positional args
      esac
      shift # move to next kv pair
  done

  # restore positional args
  set -- "${args[@]}"

  # set positionals to vars
  positional_1="${args[0]}"
  positional_2="${args[1]}"

  # validate required args
  if [[ -z "${host}" || -z "${dbname}" ]]; then
      echo "host and dbname is required"
      usage
      exit;
  fi

  # set defaults
  if [[ -z "$port" ]]; then
      port=5432;
  fi
}


function run
{
    parse_args "$@"

    echo "you passed in...\n"
    echo "positional arg 1: $positional_1"
    echo "positional arg 2: $positional_2"

    echo "named arg: host: $host"
    echo "named arg: dbname: $dbname"
    echo "named arg: port: $port"

    export PGPASSWORD=123456
    echo "clear $dbname connections"
    echo "ALTER DATABASE $dbname CONNECTION LIMIT 0;" | psql -h $host -p $port -U postgres 
    echo "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE pid <> pg_backend_pid() AND datname = '$dbname';" | psql -h $host -p $port -U postgres
    echo "recreate database"
    cat ./database/*.sql | psql -h $host -p $port -U postgres -v new_db_name=$dbname
    echo "install extensions"
    cat ./extensions/*.sql | psql -h $host -p $port -U postgres $dbname -v new_db_name=$dbname
    echo "create functions"
    cat ./functions/*.sql | psql -h $host -p $port -U $dbname $dbname
    echo "create tables"
    cat ./tables/*.sql | psql -h $host -p $port -U $dbname $dbname
    cat ./init_data/*.sql | psql -h $host -p $port -U $dbname $dbname
    cat ./test_data/*.sql | psql -h $host -p $port -U $dbname $dbname
}


run "$@";