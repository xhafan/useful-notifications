DROP VIEW IF EXISTS "LocationFilmDto";

create view "LocationFilmDto"
as
select 
    lf."Id"
    , lf."LocationId"
	, f."Name"     as "FilmName"
	, f."MainUrl"  as "FilmMainUrl"
from "LocationFilm" lf
join "Film" f on f."Id" = lf."FilmId"