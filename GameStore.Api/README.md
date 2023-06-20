# GameStore API

## Starting SQL Server

```powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

  1. `$sa_password` = `"[SA PASSWORD HERE]"`: This line is assigning a value to the variable `$sa_password`, which represents the password for the `sa` (System Administrator) account of the SQL Server. You need to replace `[SA PASSWORD HERE]` with the actual password you want to set.

  2. `docker run`: This is the command to start a new Docker container.

  3. `-e "ACCEPT_EULA=Y"`: This flag sets the environment variable `ACCEPT_EULA` to "Y", indicating acceptance of the End-User License Agreement (EULA) for the SQL Server image.

  4. `-e MSSQL_SA_PASSWORD=$sa_password`: This flag sets the environment variable `MSSQL_SA_PASSWORD` to the value stored in the `$sa_password` variable. It provides the password for the "sa" account during the SQL Server setup.

  5. `-p 1433:1433`: This flag maps the host port 1433 to the container's port 1433. Port 1433 is the default port used by SQL Server for communication.

  6. `-v sqlvolume:/var/opt/mssql`: This flag creates a Docker volume named `sqlvolume` and mounts it to the container's `/var/opt/mssql` directory. This allows persistent storage of SQL Server data outside the container.

  7. `-d`: This flag runs the container in detached mode, which means it runs in the background without attaching the terminal.

  8. `--rm`: This flag removes the container automatically when it stops. This ensures that the container is removed after you stop it, preventing accumulation of unused containers.

  9. `--name mssql`: This flag assigns the name "mssql" to the running container.

  10. `mcr.microsoft.com/mssql/server:2022-latest`: This is the name of the Docker image being used to create the container. It pulls the latest version of the SQL Server 2022 image from the Microsoft Container Registry (mcr).

Overall, this Docker command sets up and runs a containerized instance of SQL Server 2022 with the provided "sa" password, exposes the SQL Server port (1433) on the host machine, and uses a Docker volume for persistent storage.
