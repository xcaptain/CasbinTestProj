# test casbin efcore adapter

## how to test

1. git clone this project
2. do migration

    ```shell
    dotnet ef database update
    ```

    this operation will create a sqlite file named `casbin_test.sqlite3` in the current folder, you can use sqlite to inspect the database.

3. seeding the `CasbinRule` table

    ```sql
    insert into CasbinRule (Ptype, V0, V1) values ('g', 'user_1', 'admin');
    ```

4. run the sample web api

    ```shell
    dotnet run
    ```

    and then go to `https://localhost:5001/api/values`, it should return the role list of user `user_1`

## changelog

1. add support for custom casbin models, the `casbin_test.sqlite3` database now contains 2 tables
2. copy `auth_model.conf` to publish folder when running `dotnet publish`
