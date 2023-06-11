pipeline {
        agent {
                label 'docker-agent-alpine'
         }

         
    stages {

    stages {
        stage('Prepare') {
            steps {
                // Clean Jenkins workspace.
                cleanWs()
                // Clone eShopOnWeb repository.
                git 'https://github.com/ChrisMKocabas/dotnet-dockercompose-jenkins-cicd'
            }
        }

        stage('Build') {
            steps {
                // Build this project.
                sh 'dotnet restore src/Backend/Backend.csproj'
                sh 'dotnet tool restore --configfile src/Backend/.config/dotnet-tool.json'
                sh 'dotnet publish src/Backend/Backend.csproj -c Release -o out -r linux-x64 --self-contained'
                // Tell Web+ how to start this application.
                sh 'echo "Web: ASPNETCORE_ENVIRONMENT=Development . /Backend" > out/Procfile'
                // Package this project.
                sh 'cd out && zip -qr Backend-dotnet-demo.zip .'
            }
        }
    }
}
}
