CREATE OR REPLACE FUNCTION nowtms() RETURNS BIGINT AS $$ DECLARE
newval BIGINT;
BEGIN
	newval := extract(epoch from now())*1000 :: BIGINT;
RETURN newval;

END; 
$$ LANGUAGE plpgsql;
