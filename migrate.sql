insert into CasbinRule (Ptype, V0, V1) values ('g', 'user_1', 'admin');
insert into CasbinRule (Ptype, V0, V1) values ('g', 'user_1', 'user');

create table casbin_rules (
    p_type varchar(100),
    v0 varchar(100),
    v1 varchar(100),
    v2 varchar(100),
    v3 varchar(100),
    v4 varchar(100),
    v5 varchar(100)
);

insert into casbin_rules (p_type, v0, v1) values ('g', 'user_1', 'admin');
insert into casbin_rules (p_type, v0, v1) values ('g', 'user_1', 'root');
