DROP VIEW IF EXISTS "LocationFilmCinemaDto";

create view "LocationFilmCinemaDto"
as
select 
    lfc."Id"
    , lfc."LocationFilmId"      as "LocationFilmDtoId"
    , c."Name"                  as "CinemaName"
from "LocationFilmCinema" lfc
join "Cinema" c on c."Id" = lfc."CinemaId"