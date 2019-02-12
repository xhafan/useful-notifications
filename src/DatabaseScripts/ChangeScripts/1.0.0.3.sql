
    drop table if exists "Cinema" cascade;

    drop table if exists "Country" cascade;

    drop table if exists "Film" cascade;

    drop table if exists "FilmRating" cascade;

    drop table if exists "Location" cascade;

    drop table if exists "LocationFilm" cascade;

    drop table if exists "LocationFilmCinema" cascade;

    drop table if exists hibernate_unique_key cascade;

    create table "Cinema" (
        "Id" int4 not null,
       "Name" text not null,
       primary key ("Id")
    );

    create table "Country" (
        "Id" int4 not null,
       "Name" text not null,
       "Code" text not null,
       primary key ("Id")
    );

    create table "Film" (
        "Id" int4 not null,
       "Name" text not null,
       "MainUrl" text not null,
       primary key ("Id")
    );

    create table "FilmRating" (
        "Id" int4 not null,
       "Source" varchar(255) not null,
       "Rating" decimal(19,5) not null,
       "Url" text not null,
       "FilmId" int4,
       primary key ("Id")
    );

    create table "Location" (
        "Id" int4 not null,
       "NameOrPostCode" text not null,
       "LastUpdated" timestamp not null,
       "CountryId" int4 not null,
       primary key ("Id")
    );

    create table "LocationFilm" (
        "Id" int4 not null,
       "FilmId" int4 not null,
       "LocationId" int4,
       primary key ("Id")
    );

    create table "LocationFilmCinema" (
        "Id" int4 not null,
       "CinemaId" int4 not null,
       "LocationFilmId" int4,
       primary key ("Id")
    );

    alter table "FilmRating" 
        add constraint FK_7B19BD1A 
        foreign key ("FilmId") 
        references "Film";

    alter table "Location" 
        add constraint FK_Location_Country 
        foreign key ("CountryId") 
        references "Country";

    alter table "LocationFilm" 
        add constraint FK_LocationFilm_Film 
        foreign key ("FilmId") 
        references "Film";

    alter table "LocationFilm" 
        add constraint FK_7D72271C 
        foreign key ("LocationId") 
        references "Location";

    alter table "LocationFilmCinema" 
        add constraint FK_LocationFilmCinema_Cinema 
        foreign key ("CinemaId") 
        references "Cinema";

    alter table "LocationFilmCinema" 
        add constraint FK_65FC009 
        foreign key ("LocationFilmId") 
        references "LocationFilm";

    create table hibernate_unique_key (
         next_hi int4 
    );

    insert into hibernate_unique_key values ( 1 );
