# Agent Portal App
Hello, thank you for allowing me to complete this coding challenge. Please follow the instructions below to run the app.

I used the following parts to build this project:
* An asp-dotnetcore server which is responsible for serving the static assets + acting as the web-api.
* A front-end single-page app built using Angular + typescript & scss.
* An [entity-framework in-memory databse provider](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli) which serves as the data-store. This is to aide with being able to distribute and 
* Azure blob storage - used as the image store.


*Some things to note:*
* The in-memory database provider does not persist data, hence all entries will be lost when the app finishes execution.
* Also please note, as per the documentation, the in-memory db provider does not uphold any referencial integrity on stored entities
* Out of the box, the app is using an Azure blob storage service created and hosted from my Azure subscription. No setup work should be required at your end

## Before you run

If you wish to checkout and execute the app, please ensure you have the `dotnetcore` SDK & `nodejs` tools installed on your machine.

Please ensure the runtimes match or are newer than the following versions listed below:

```
> dotnet --version
3.1.100

> node -v
v12.14.1

> npm -v
6.13.4
```

## Compiling and executing the application
On a terminal of your choice, please navigate to the root of the directory where you have checked out the repository from github.

### Step-1. Compiling the front-end client app

You will need to build the front-end client app first using nodejs.

The node client application source code is located in the following directory `./AgentPortal/AgentPortal/Client`.

1. In the root directory of the application please execute the following command to restore all dependencies:

```
> npm install --prefix ./AgentPortal/AgentPortal/Client
```
**Warning!** I have sometimes found the `--prefix` operator does not work on windows machines. If npm reports an error, please `cd` into the `./AgentPortal/AgentPortal/Client` directory and simply run `npm install` (without the prefix argument).

2. The app uses the [Angular CLI](https://cli.angular.io/) tool (installed as a dependency) to compile itself. Assuming you are still in the root directory of the repo, please run
```
> npm run --prefix ./AgentPortal/AgentPortal/Client build
```
**Warning!** If you had an error with the `--prefix` operator in the previous step, it won't work here either :( hence as before, please just `cd` into the `./AgentPortal/AgentPortal/Client` directory and simply run `npm run build` (without the prefix argument).

Upon completion, the ng CLI tools should store the static assets in the following location `./AgentPortal/AgentPortal/wwwroot/js/portalClient`

### Step-2. Compiling the dotnetcore solution

After completing step-1 you may now run the application. 

The dotnetcore solution file is located along the path `./AgentPortal/AgentPortal.sln`.

The asp dotnetcore server project is located along the path `./AgentPortal/AgentPortal/AgentPortal.csproj`.

**Note:** Out-of-the-box, the server tries to connect to the following local ports `5000` && `5001`
 
Please run:
```
> dotnet build ./AgentPortal/AgentPortal.sln
```

This should restore all dependencies and build all the projects associated with the solution

You may then run the application by specifying which project in the solution is to be run like so:
```
> dotnet run --project ./AgentPortal/AgentPortal/AgentPortal.csproj
```

Assuming everything has run successfully, you may browse the application along the path `https://localhost:5001` or `http://localhost:5000`

### Step-3. Running unit tests

You may run all unit tests in the solution like so:

```
> dotnet test ./AgentPortal/AgentPortal.sln
```


## Key decisions made during this build

* The primary technologies used, dotnetcore and nodejs all have cross platform compatibility support to allow containerizing the solution.
* I took the liberty of experimenting with github actions and compiled a small build pipeline, located at the path `.github/workflows/build-site-on-push-workflow.yaml` to demonstrate my project setup is CLI ready. I split it into two build-jobs so that the app build and the unit tests can run in parallel to save on build time. You can browse my previous build attempts from the `actions` tab on the github repo.
* From an Architectural standpoint, I have aimed to isolate platform level dependencies within the solution into their own projects. Note that both the Entity framework DB provider and the azure SDK are hosted within their own projects and exposed either via the lens of an interface or using an adapter. This prevents adjacent assemblies from needing to maintain a reference to their SDK's hence allowing swapping to an alternate tech in the future.
* All dependencies are registered and injected by the asp `WebHostBuilder`. I extensively use constructor based DI as I find this excellent at decoupling dependencies and also facilitates unit testing.
* This was actually the first time I used the entity-framework in memory DB provider. In lieu of it not supporting referential table integrity, please do not be alarmed at the way I have implemented some of my validation checks on entities, I would normally depend on the schema definitions to do this for me when using a full SQL DB solution.
* I use `Moq` and `XUnit` as my testing platforms. I have tried to maintain test coverage of all my logic and DAL layers.
* I like to seperate my REST request/response types from directly using DAL entity types. This allows augmenting the REST types to suppert hypermedia links and allows both areas to be managed independantly. Also note that by being housed within the `AgentPortal.Domain` project, a library that contains all my core types with no external dependencies, makes it easy to publish this library and distribute it as nuget pacakge. This way Clients I write can consume the REST response/request packets making serializing/de-serializing much easier.
* I feel with the way I have laid out the application, should make it pretty easy to split the code into a client, server and static-content server project in the future. This allows each area to grow and be managed independantly by different teams.
* I like to group and compile all my sass files from one entry point VS going with the angular default of having each component refernce a .scss file. I have a couple of reasons for this, one being I am a huge fan of the [SMACSS](http://smacss.com/book/categorizing) methodology when it comes to styling. The other being, in a real-world scenario I usually like to be able to inject a device-specific css style-sheet to cater for a better experience, but again this is one of many approaches.


Thanks again for the exercise, it was a lot of fun :)

Regards