DROP VIEW IF EXISTS "LocationFilmDto";

create view "LocationFilmDto"
as
select 
    lf."Id"
    , lf."LocationId"
    , l."CountryId"
    , l."NameOrPostCode"    as "LocationNameOrPostCode"
	, f."Name"              as "FilmName"
	, f."MainUrl"           as "FilmMainUrl"
from "LocationFilm" lf
join "Location" l on l."Id" = lf."LocationId"
join "Film" f on f."Id" = lf."FilmId"
