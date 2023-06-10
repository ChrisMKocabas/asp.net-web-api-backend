pipeline{
    agent any
    stages {
        stage("verify tooling") {
            steps {
                sh '''
                echo "Verifying tooling"
                docker version
                docker info
                docker compose version
                curl --version
                apt-get install -y jq
                jq --version
                '''
            }
        }
    }
}