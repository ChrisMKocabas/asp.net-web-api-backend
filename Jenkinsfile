pipeline {
    agent {label 'docker-agent-alpine'}
    stages {
        stage("verify tooling") {
            steps {
                sh '''
                echo "Verifying tooling"
                docker build
                docker version
                docker info
                docker compose version
                curl --version
                jq --version
                '''
                
            }
        }
    }
}