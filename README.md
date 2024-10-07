# AWAQ Dashboard
Dashboard to visualize user progress as they go through their OnBoarding process with AWAQ.

## How to run

1. Create the appsettings.json file -> you can use appsettings.json.example as a template. Make sure to provide valid Azure AD values.<br><br>
2. Create the db either locally or in the cloud and provide the connection string at the appsettings.json.<br><br>
3. Run the application using Visual Studio or run the following command in the terminal at the root directory:
```bash
dotnet watch run
```
<br>

4. To run the videogame, the API url will be needed. Either change the localhost url for the IP address of the computer at the Properties/launchSettings.json file or use NGrok to expose the local web server to the internet. After installing and setting up NGrok, use the following command (replacing the <domain> and <port>):
  
```bash
ngrok http --domain=<domain> <port>
```
<br>

5. In Unity, change the API url accordingly and build the game to import it on XCode.


More about the videogame setup checkout the repo: [AWAQ Game](https://github.com/Oscar-gg/awak-game).

## Tech Stack

- [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet)
- [Bootstraps](https://getbootstrap.com/docs/5.3/getting-started/introduction/)
- [C#](https://dotnet.microsoft.com/es-es/languages/csharp)
- [MySQL](https://www.mysql.com/)
- [Tailwind](https://tailwindcss.com/)
  
## Development Team

| Name | Github | Email |
| --- | --- | --- |
| Oscar Arreola | [@Oscar-gg](https://github.com/Oscar-gg) | a01178076@tec.mx |
| Alejandra Coeto | [@Ale-Coeto](https://github.com/AleCoeto) | a01285221@tec.mx |
| Pablo García | [@pagmtz03](https://github.com/pagmtz03) | a01412895@tec.mx |
| Axel Grande | [@4xlRose](https://github.com/4xlRose) | a01611811@tec.mx |
| Axel Iparrea | [@axeliparrea](https://github.com/axeliparrea) | a00836682@tec.mx |

## Preview
[Demo](https://youtu.be/QS9yNjd9SyQ)
<img width="1404" alt="Captura de pantalla 2024-10-06 a la(s) 11 42 54 p m" src="https://github.com/user-attachments/assets/577c9c10-5fd5-4c11-a839-471fbf36d155">
<img width="1470" alt="Captura de pantalla 2024-06-07 a la(s) 3 17 00 p m" src="https://github.com/user-attachments/assets/0a7fd4b8-a7ff-4623-b53b-c3c15593b5c9">
<img width="1407" alt="Captura de pantalla 2024-10-06 a la(s) 11 44 56 p m" src="https://github.com/user-attachments/assets/fd16e527-cd6b-469b-84e4-9bf25f6a3625">
<img width="1400" alt="Captura de pantalla 2024-10-06 a la(s) 11 44 25 p m" src="https://github.com/user-attachments/assets/833146b1-0560-4d49-a33b-9c6808a271bc">

