DROP VIEW IF EXISTS "FilmRatingDto";

create view "FilmRatingDto"
as
select
    fr."Id"
    , lf."Id"  as "LocationFilmDtoId"
    , "Source"  as "RatingSource"
    , "Url"     as "FilmUrl"
    , "Rating"
from "LocationFilm" lf 
join "FilmRating" fr on fr."FilmId" = lf."FilmId" 
