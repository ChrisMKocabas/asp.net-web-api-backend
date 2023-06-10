## Creating and running Jenkins in Docker for CI/CD Pipeline

There are several Docker images of Jenkins available.

The recommended Docker image to use is the Official jenkins/jenkins image (from the Docker Hub repository). This image contains the current Long-Term Support (LTS) release of Jenkins (which is production-ready). However this image doesn’t have docker CLI inside it and is not bundled with frequently used Blue Ocean plugins and features. This means that if you want to use the full power of Jenkins and Docker you may want to go through described below installation process.

A new jenkins/jenkins image is published each time a new release of Jenkins Docker is published. You can see a list of previously published versions of the jenkins/jenkins image on the tags page.

## On macOS and Linux:

- Open up a terminal window.

- Create a bridge network in Docker using the following docker network create command:

"docker network create jenkins"

- In order to execute Docker commands inside Jenkins nodes, download and run the docker:dind Docker image using the following docker run command:

docker run \
  --name jenkins-docker \
  --rm \
  --detach \
  --privileged \
  --network jenkins \
  --network-alias docker \
  --env DOCKER_TLS_CERTDIR=/certs \
  --volume jenkins-docker-certs:/certs/client \
  --volume jenkins-data:/var/jenkins_home \
  --publish 2376:2376 \
  docker:dind \
  --storage-driver overlay2

Purpose of dashed lines: 

1 - ( Optional ) Specifies the Docker container name to use for running the image. By default, Docker will generate a unique name for the container.
2 - ( Optional ) Automatically removes the Docker container (the instance of the Docker image) when it is shut down.
3 - ( Optional ) Runs the Docker container in the background. This instance can be stopped later by running docker stop jenkins-docker.
4 - Running Docker in Docker currently requires privileged access to function properly. This requirement may be relaxed with newer Linux kernel versions.
5 - This corresponds with the network created in the earlier step.
6 - Makes the Docker in Docker container available as the hostname docker within the jenkins network.
7 - Enables the use of TLS in the Docker server. Due to the use of a privileged container, this is recommended, though it requires the use of the shared volume described below. This environment variable controls the root directory where Docker TLS certificates are managed.
8 - Maps the /certs/client directory inside the container to a Docker volume named jenkins-docker-certs as created above.
9 - Maps the /var/jenkins_home directory inside the container to the Docker volume named jenkins-data. This will allow for other Docker containers controlled by this Docker container’s Docker daemon to mount data from Jenkins.
10 - ( Optional ) Exposes the Docker daemon port on the host machine. This is useful for executing docker commands on the host machine to control this inner Docker daemon.
11 - The docker:dind image itself. This image can be downloaded before running by using the command: docker image pull docker:dind.
12 - The storage driver for the Docker volume. See "Docker storage drivers" for supported options.

### Note: If copying and pasting the command snippet above does not work, try copying and pasting this annotation-free version here:

docker run --name jenkins-docker --rm --detach \
  --privileged --network jenkins --network-alias docker \
  --env DOCKER_TLS_CERTDIR=/certs \
  --volume jenkins-docker-certs:/certs/client \
  --volume jenkins-data:/var/jenkins_home \
  --publish 2376:2376 \
  docker:dind --storage-driver overlay2


## Customise official Jenkins Docker image, by executing below two steps:

- Create Dockerfile with the following content:

FROM jenkins/jenkins:2.401.1
USER root
RUN apt-get update && apt-get install -y lsb-release
RUN curl -fsSLo /usr/share/keyrings/docker-archive-keyring.asc \
  https://download.docker.com/linux/debian/gpg
RUN echo "deb [arch=$(dpkg --print-architecture) \
  signed-by=/usr/share/keyrings/docker-archive-keyring.asc] \
  https://download.docker.com/linux/debian \
  $(lsb_release -cs) stable" > /etc/apt/sources.list.d/docker.list
RUN apt-get update && apt-get install -y docker-ce-cli
USER jenkins
RUN jenkins-plugin-cli --plugins "blueocean docker-workflow"

- Build a new docker image from this Dockerfile and assign the image a meaningful name, e.g. "myjenkins-blueocean:2.401.1-1":

docker build -t jenkins-blueocean:2.401.1-1 .

- Keep in mind that the process described above will automatically download the official Jenkins Docker image if this hasn’t been done before.

## Run your own myjenkins-blueocean:2.401.1-1 image as a container in Docker using the following docker run command:

docker run \
  --name jenkins-blueocean \
  --restart=on-failure \
  --detach \
  --network jenkins \
  --env DOCKER_HOST=tcp://docker:2376 \
  --env DOCKER_CERT_PATH=/certs/client \
  --env DOCKER_TLS_VERIFY=1 \
  --publish 8080:8080 \
  --publish 50000:50000 \
  --volume jenkins-data:/var/jenkins_home \
  --volume jenkins-docker-certs:/certs/client:ro \
  jenkins-blueocean:2.401.1-1 

Line 1 - ( Optional ) Specifies the Docker container name for this instance of the Docker image.
Line 2 - Always restart the container if it stops. If it is manually stopped, it is restarted only when Docker daemon restarts or the container itself is manually restarted.
Line 3 - ( Optional ) Runs the current container in the background (i.e. "detached" mode) and outputs the container ID. If you do not specify this option, then the running Docker log for this container is output in the terminal window.
Line 4 - Connects this container to the jenkins network defined in the earlier step. This makes the Docker daemon from the previous step available to this Jenkins container through the hostname docker.
Line 5 - Specifies the environment variables used by docker, docker-compose, and other Docker tools to connect to the Docker daemon from the previous step.
Line 6 - Maps (i.e. "publishes") port 8080 of the current container to port 8080 on the host machine. The first number represents the port on the host while the last represents the container’s port. Therefore, if you specified -p 49000:8080 for this option, you would be accessing Jenkins on your host machine through port 49000.
Line 7 - ( Optional ) Maps port 50000 of the current container to port 50000 on the host machine. This is only necessary if you have set up one or more inbound Jenkins agents on other machines, which in turn interact with your jenkins-blueocean container (the Jenkins "controller"). Inbound Jenkins agents communicate with the Jenkins controller through TCP port 50000 by default. You can change this port number on your Jenkins controller through the Security page. If you were to change the TCP port for inbound Jenkins agents of your Jenkins controller to 51000 (for example), then you would need to re-run Jenkins (via this docker run … command) and specify this "publish" option with something like --publish 52000:51000, where the last value matches this changed value on the Jenkins controller and the first value is the port number on the machine hosting the Jenkins controller. Inbound Jenkins agents communicate with the Jenkins controller on that port (52000 in this example). Note that WebSocket agents do not need this configuration.
Line 8 - Maps the /var/jenkins_home directory in the container to the Docker volume with the name jenkins-data. Instead of mapping the /var/jenkins_home directory to a Docker volume, you could also map this directory to one on your machine’s local file system. For example, specifying the option
--volume $HOME/jenkins:/var/jenkins_home would map the container’s /var/jenkins_home directory to the jenkins subdirectory within the $HOME directory on your local machine, which would typically be /Users/<your-username>/jenkins or /home/<your-username>/jenkins. Note that if you change the source volume or directory for this, the volume from the docker:dind container above needs to be updated to match this.
Line 9 - Maps the /certs/client directory to the previously created jenkins-docker-certs volume. This makes the client TLS certificates needed to connect to the Docker daemon available in the path specified by the DOCKER_CERT_PATH environment variable.
Line 10 - The name of the Docker image, which you built in the previous step.

### Note: If copying and pasting the command snippet above does not work, try copying and pasting this annotation-free version here:

docker run --name jenkins-blueocean --restart=on-failure --detach \
  --network jenkins --env DOCKER_HOST=tcp://docker:2376 \
  --env DOCKER_CERT_PATH=/certs/client --env DOCKER_TLS_VERIFY=1 \
  --publish 8080:8080 --publish 50000:50000 \
  --volume jenkins-data:/var/jenkins_home \
  --volume jenkins-docker-certs:/certs/client:ro \
  jenkins-blueocean:2.401.1-1
  


## Get the jenkins admin password 
π
"docker exec jenkins-blueocean cat /var/jenkins_home/secrets/initialAdminPassword"

## Connect to the Jenkins
https://localhost:8080/

## alpine/socat container to forward traffic from Jenkins to Docker Desktop on Host Machine

https://stackoverflow.com/questions/47709208/how-to-find-docker-host-uri-to-be-used-in-jenkins-docker-plugin

"docker run -d --restart=always -p 127.0.0.1:2376:2375 --network jenkins -v /var/run/docker.sock:/var/run/docker.sock alpine/socat tcp-listen:2375,fork,reuseaddr unix-connect:/var/run/docker.sock"
"docker inspect <container_id> | grep IPAddress"


- Go to Dashboards / Manage Jenkins / Clouds and create a Docker cloud with the host URI you got from alpine/socat image container we just created with 2375 port number. Make sure to check Enabled checkbox.

- Next create a Docker Agent Template, name the label and name line same. And as Docker Image lets use: jenkins/agent:alpine-jdk11
 Instance capacity: lets say 4
 Remote File System Root: /home/jenkins



### Ta-da! Now start pulling jenkins agents of your choice (python anyone?) and get that environment set up!