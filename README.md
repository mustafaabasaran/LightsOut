# LightsOut

[![BuildLightsOut](https://github.com/mustafaabasaran/LightsOut/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/mustafaabasaran/LightsOut/actions/workflows/build.yml)
[![BuildLightsOutWindowsForm](https://github.com/mustafaabasaran/LightsOut/actions/workflows/buildwindowsform.yml/badge.svg?branch=main)](https://github.com/mustafaabasaran/LightsOut/actions/workflows/buildwindowsform.yml)
[![TestLightsIntegrationTests](https://github.com/mustafaabasaran/LightsOut/actions/workflows/testintegration.yml/badge.svg?branch=main)](https://github.com/mustafaabasaran/LightsOut/actions/workflows/testintegration.yml)
[![TestLightsOutUnitTests](https://github.com/mustafaabasaran/LightsOut/actions/workflows/test.yml/badge.svg?branch=main)](https://github.com/mustafaabasaran/LightsOut/actions/workflows/test.yml)
[![TestLightsOutWindowsForm](https://github.com/mustafaabasaran/LightsOut/actions/workflows/testwindowsform.yml/badge.svg?branch=main)](https://github.com/mustafaabasaran/LightsOut/actions/workflows/testwindowsform.yml)


Lights Out is a puzzle game consisting of an n x n grid of lights, in this implementation it is 5 x 5. At the beginning of the game, a random number of lights between 1 and 10 are switched on. When a light is pressed, this light and the four adjacent lights are toggled, i.e., they are switched on if they were off and switched off otherwise. The purpose of the game is to switch all the lights off.

![Image from Wikipedia.](https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/LightsOutIllustration.svg/400px-LightsOutIllustration.svg.png)

This game is developed with .Net 5 and use Sql Server as a database. It has two component in it. First one is api which contains game settings, second one is windows form application.  

## How to run? 

- Clone it with `https://github.com/mustafaabasaran/LightsOut.git`
- Create a database called `LightsOut` and execute `scripts.sql`in that database which is located in LightsOut folder.
- After running the script run the api. You can run the api with two methods.
    - If you want to run the api without creating container, you should go to `./LightsOut.Api` folder and than edit `LightsOutConnectionString` section in the `appsettings.json`. You can configure it with your database settings.
    After correcting the connection string then run `dotnet build`. After building you can run the api with `dotnet run`
    - If you want to run the api with container you need to create image with `docker build -t lightsoutapi -f LightsOut.Api/Dockerfile .` After image is created run `docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development --name lightout lightsoutapi`. If you forgot to change connection string you can give correct string as a environment when running the container.
- At this point you should be able to navigate `http://localhost:8080/swagger/index.html` and see swagger interface. You can test the api with this interface.
- After running the api you can run the application. Go to `./LightsOut.WindowsForm` folder. Build the application with `dotnet build` command. After building you can run the application with `dotnet run` 
- Enjoy your game is ready. You can play it now :)
- Api application also have unit and integration tests. You can find the unit tests under `./LightsOut.Api.UnitTests` folder, integration tests under `./LightsOut.Api.IntegrationTests` folder.
- Windows form also have unit tests. You can find the unit tests under `./LightsOut.Api.UnitTests`.