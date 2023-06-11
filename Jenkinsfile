pipeline{
    agent {
        label 'docker-agent-alpine'
    }
    stages {
        stage("verify tooling") {
            steps {
                sh '''
                echo "Verifying tooling"
                docker version
                docker info
                docker compose version
                curl --version
                - apt-get update 
                - apt-get -y install jq
                jq --version
                '''
            }
        }
    }
}