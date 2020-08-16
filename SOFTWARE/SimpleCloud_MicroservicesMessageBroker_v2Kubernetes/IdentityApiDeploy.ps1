#cd 'D:\STUDIA\PRACA MAGISTERSKA\SOFTWARE\SimpleCloud_MicroservicesMessageBroker_v2Kubernetes'

# identity-api

docker build -t rsazor12/identityapi:v8 -f IdentityApiDockerfile .

docker image ls | head -n2

docker push rsazor12/identityapi:v8

#docker run --name identityapi_v5 -p 13000:80 rsazor12/identityapi:v5 


#machinelearning-api

docker build -t rsazor12/machinelearningapi:v3 -f MachineLearningDockerfile .

docker image ls | head -n2

docker push rsazor12/machinelearningapi:v3


#payment-api

docker build -t rsazor12/paymentapi:v2 -f PaymentDockerfile .

docker image ls | head -n2

docker push rsazor12/paymentapi:v2








##################KUBERNETES################################
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

#machinelearning-api-db
kubectl apply -f machinelearning-api-db-deployment.yaml

kubectl apply -f machinelearning-api-db-nodeport.yml


#machinelearning-api
kubectl apply -f machinelearning-api-configmap.yml 

kubectl apply -f machinelearning-api-deployment.yaml

kubectl apply -f machinelearning-api-nodeport.yml

kubectl rollout restart deployment machinelearning-api


#payment-api-db
kubectl apply -f ./paymentapi/payment-api-db-deployment.yaml

kubectl apply -f ./paymentapi/payment-api-db-nodeport.yml


#payment-api
kubectl apply -f ./paymentapi/payment-api-configmap.yml 

kubectl apply -f ./paymentapi/payment-api-deployment.yaml

kubectl apply -f ./paymentapi/payment-api-nodeport.yml

kubectl rollout restart deployment payment-api





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