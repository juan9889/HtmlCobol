# HtmlCobol
Cobol inside Html

Works server-side as a .net cli application and client-side as a WASM browser app

# Supported keywords (for now)
SET, DISPLAY, COMPUTE, GOTO(in progress)

![Captura de Pantalla 2022-05-18 a la(s) 14 44 35](https://user-images.githubusercontent.com/48728949/169167250-45dc08a5-06a4-403a-8ebf-d1a855baebfc.png)

# How to use (server-side, .net cli)

1- Install .net core 6 sdk

2- Edit the /HtmlCobol/Program.cs file, set the example.html route

3- cd into the project folder, /HtmlCOBOL

4- 'dotnet run' in terminal

5- Go to localhost:6969 in your browser

6- Edit the example.html file, save and refresh the browser to see the changes


# How to use (client-side, WASM)

Due security features, you can't just open the html files and serve local resources on your browser so you have to deploy this to a server as static files

1- Install .net core 6 sdk

2- Install dotnet wasm-tools 

3- cd into the project folder, /Client

3- 'dotnet publish -c Release' in terminal

4- deploy the files in /Client/bin/Release/net6.0/publish/wwwroot in a server

5- create an 'example.html' file that contains cobol code in the same folder that contains index.html

