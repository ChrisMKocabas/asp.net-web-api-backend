pipeline {
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
                '''
                sh 'apk add --update jq && jq --version'
            }
        }
    }
}