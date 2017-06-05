drop table if exists notificaciones;
create table notificaciones (
	codigo  int(11) unsigned not null auto_increment ,
	nombre  varchar(60) character set utf8 collate utf8_general_ci not null ,  
	descripcion  varchar(320) character set utf8 collate utf8_general_ci null default null ,
	destinatarios  varchar(512) character set utf8 collate utf8_general_ci not null ,
	asunto  varchar(60) character set utf8 collate utf8_general_ci not null,
	contenido  varchar(2048) character set utf8 collate utf8_general_ci not null ,
	tipodestinatarios  tinyint(4) not null ,
	tipocontenido  tinyint(4) not null ,
	primary key (codigo)
);
