#cd 'D:\STUDIA\PRACA MAGISTERSKA\SOFTWARE\SimpleCloud_MicroservicesMessageBroker_v2Kubernetes'


docker build -t rsazor12/identityapi:v6 -f IdentityApiDockerfile .

docker image ls | head -n2

docker push rsazor12/identityapi:v6

#docker run --name identityapi_v5 -p 13000:80 rsazor12/identityapi:v5 

# rabbitmq
kubectl apply -f rabbitmq-deployment.yaml

kubectl apply -f rabbitmq-nodeport.yml

kubectl apply -f rabbitmq-cluster-ip.yml


#identity-api-db
kubectl apply -f identity-api-db-deployment.yaml

kubectl apply -f identity-api-db-nodeport.yml

# Identity-api
kubectl apply -f identity-api-configmap.yml 

kubectl apply -f identity-api-deployment.yaml

kubectl apply -f identity-api-nodeport.yml

kubectl rollout restart deployment identity-api



# Helpers:
curl --location --request POST 'http://localhost:30000/api/clients' --header 'Content-Type: application/json' \--data-raw '{
	"Email": "client5@mail.com",
	"Password": "password",
	"Name": "Name",
	"Surname": "surname"
}'

curl

kubectl exec --stdin --tty identity-api-687669f6ff-95p4x -- /bin/bash

kubectl get pods -l run=identity-api-db-8488fc559f-9gsj8 -o wide







#kubectl apply -f identity-api-deployment.yaml