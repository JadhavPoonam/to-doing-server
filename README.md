# to-doing-server

## Docker Commands

### Build
docker build . -t to-doing-server-image:latest

### Run
docker-compose up -d
Note: If you want to build everytime you up the container, you have to have a build context. If you already have an image in docker hub, you can use the image name in the docker-compose.yml

### Pushing to docker hub
docker login
docker push <tagname>
Note: Tag name should have the format username/image-name 
