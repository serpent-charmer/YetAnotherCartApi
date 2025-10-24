create sequence users_column_name_seq
    as integer;

alter sequence users_column_name_seq owner to postgres;

create sequence items_id_seq
    as integer;

alter sequence items_id_seq owner to postgres;

create table if not exists users
(
    id       integer default nextval('shop.users_column_name_seq'::regclass) not null,
    uid      uuid    default gen_random_uuid(),
    username varchar
);

alter table users
    owner to postgres;

alter sequence users_column_name_seq owned by users.id;

create table if not exists widgets
(
    widget_name varchar,
    id          integer                  default nextval('shop.items_id_seq'::regclass) not null,
    price       integer,
    uid         uuid                     default gen_random_uuid(),
    description varchar,
    created_at  timestamp with time zone default now(),
    user_id     uuid
);

alter table widgets
    owner to postgres;

alter sequence items_id_seq owned by widgets.id;

create table if not exists cart_content
(
    id        serial,
    user_id   integer,
    widget_id integer
);

alter table cart_content
    owner to postgres;

create table if not exists "order"
(
    id        serial,
    status    varchar default 'transit'::character varying not null,
    widget_id uuid,
    uid       uuid    default gen_random_uuid()            not null,
    arrive_at uuid,
    buyer     uuid
);

alter table "order"
    owner to postgres;

create table if not exists invoice
(
    id       serial,
    price    integer,
    order_id uuid,
    buyer    uuid,
    seller   uuid
);

alter table invoice
    owner to postgres;

create table if not exists warehouse
(
    id      serial,
    uid     uuid default gen_random_uuid() not null,
    address varchar
);

alter table warehouse
    owner to postgres;

create table if not exists capabilities
(
    id         serial,
    user_id    uuid,
    capability varchar
);

alter table capabilities
    owner to postgres;

create or replace view v_cart_content(price, widget_name, uid, user_id) as
SELECT widgets.price,
       widgets.widget_name,
       widgets.uid,
       users.uid AS user_id
FROM shop.users
         JOIN shop.cart_content ON users.id = cart_content.user_id
         JOIN shop.widgets ON widgets.id = cart_content.widget_id;

alter table v_cart_content
    owner to postgres;

create or replace view invoice_info(seller, buyer, order_id, price) as
SELECT s.uid AS seller,
       b.uid AS buyer,
       o.uid AS order_id,
       w.price
FROM shop."order" o
         JOIN shop.widgets w ON o.widget_id = w.uid
         JOIN shop.users s ON w.user_id = s.uid
         JOIN shop.users b ON o.buyer = b.uid;

alter table invoice_info
    owner to postgres;

