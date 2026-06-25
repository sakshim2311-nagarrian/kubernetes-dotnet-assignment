# Kubernetes Deployment Documentation

# 1. Requirement Understanding

The objective of this assignment is to deploy a backend application using Kubernetes and demonstrate container orchestration capabilities.

The solution should demonstrate:

- Containerization using Docker
- Deployment of microservices
- Database deployment
- Service discovery
- External access using Ingress and LoadBalancer Service
- Self healing
- Persistent storage
- Rolling deployments
- Horizontal scaling

The application should be able to:

- Serve API requests
- Retrieve records from database
- Recover automatically after pod failures
- Maintain database data after restart

---

# 2. Assumptions

The following assumptions were considered:

- Application is a .NET Web API
- Docker images are available in Docker Hub
- Kubernetes cluster is available
- Database credentials are managed using Kubernetes Secrets
- Application configuration is provided through Kubernetes manifests
- Persistent volume is available for database storage

---

# 3. Solution Overview

## Architecture

Client
|
|
LoadBalancer Service (External Access)
|
|
Ingress Controller (HTTP Routing)
|
|
Kubernetes Service
|
|
.NET API Deployment
|
|
PostgreSQL Deployment
|
Persistent Volume

## Application Layer

The .NET API is packaged as a Docker image.

The Kubernetes Deployment manages API pods.

Deployment provides:

- Replica management
- Self healing
- Rolling updates

## Database Layer

PostgreSQL is deployed inside Kubernetes.

Database storage is handled using Persistent Volume Claims.

This ensures:

- Data survives pod restart
- Database pod can be recreated without losing data

## Networking

Kubernetes Service provides internal communication between components.

Ingress provides external HTTP routing. The Ingress Controller is exposed through a Kubernetes LoadBalancer Service, which provides
the external IP used to access the application.

Example flow:

User
|
LoadBalancer External IP
|
Ingress Controller
|
Service
|
API Pods
|
Database Service
|
PostgreSQL Pod

---

# 4. Kubernetes Resources Used

## Deployment

Used for managing application replicas.

Benefits:

- Automatic pod replacement
- Rolling updates
- Desired state management

## Service

Used for stable networking.

Benefits:

- Pod discovery
- Load balancing

## LoadBalancer Service

Used for exposing the application outside the Kubernetes cluster.

Benefits:

- Public IP allocation
- External API access
- Traffic distribution across pods

## Ingress

Used to expose application externally.

Benefits:

- HTTP routing
- Single entry point

## Secret

Used for storing sensitive information.

Example:

- Database password

## Persistent Volume

Used for database durability.

Benefits:

- Data persistence
- Storage separation from pod lifecycle

## Horizontal Pod Autoscaler

Automatically adjusts pod replicas based on resource usage.

During high traffic:

CPU increases
|
HPA detects load
|
New pods created

After traffic reduces:

CPU decreases
|
HPA scales down gradually

---

# 5. Self Healing Demonstration

Kubernetes continuously monitors desired state.

If API pod is deleted:

kubectl delete pod

Kubernetes automatically creates a replacement pod.
Similarly, database pod recreation is handled by Deployment while Persistent Volume keeps data safe.

---

# 6. Deployment Strategy

Rolling Update strategy is used.

During deployment update:

1. New pod version is created
2. Health checks verify readiness
3. Old pods are removed

This ensures minimum downtime.

Rollout history:

kubectl rollout history deployment serviceapi

---

# 7. Horizontal Scaling

HPA configuration allows automatic scaling.

Example load generation:

ab -n 10000 -c 50 http://<external-ip>/api/records

Expected behaviour:

- More requests increase CPU
- Additional replicas are created
- Traffic is distributed

---

# 8. Resource Utilization Justification

## Kubernetes Cluster

Used because it provides:

- Container orchestration
- Scaling
- Availability
- Self healing

## Docker

Used because it provides:

- Consistent runtime environment
- Portable deployment

## PostgreSQL

Used because:

- Relational storage required
- Persistent data handling

## LoadBalancer Service

The LoadBalancer service was used because the API required external accessibility.

It provides:
- Public endpoint for API testing
- Integration with cloud provider networking
- Traffic routing to backend pods

## Ingress

Used because:

- Provides external access
- Simplifies routing

## HPA

Used because:

- Handles variable traffic
- Optimizes resource usage

---

# 9. FinOps Considerations and Cost Optimization
The Kubernetes deployment follows FinOps principles by controlling resource consumption and avoiding unnecessary infrastructure costs.

Resource Requests and Limits
CPU and memory requests and limits are configured for the API tier.

Example:

resources:
  requests:
    memory: "256Mi"
    cpu: "250m"
  limits:
    memory: "512Mi"
    cpu: "500m"

Requests:

Define the minimum guaranteed resources required by the application.
Help Kubernetes schedule pods efficiently on available nodes.

Limits:

- Prevent a pod from consuming excessive CPU or memory.
- Protects cluster stability and reduces unexpected resource consumption.

Kubernetes Cost Optimization Opportunities :

1. Right-sizing CPU and Memory Resources
Application containers are assigned resource requests and limits based on observed usage.

Benefits:

- Avoids over-provisioning
- Improves node utilization
- Reduces unnecessary cloud infrastructure cost

2. Horizontal Pod Autoscaling (HPA)
HPA automatically adjusts the number of API pods based on CPU utilization.

Benefits:

- During low traffic, fewer pods run and reduce resource usage.
- During high traffic, additional pods are created automatically.
- Avoids maintaining unnecessary replicas.

3. Efficient Scaling and Replica Management
Deployment replicas are controlled based on application requirements.

Benefits:

- Maintains availability without running excess pods.
- Reduces compute waste.
- Implemented Resource Optimization

The following optimizations were implemented:

- CPU and memory requests/limits configured for API pods.
- HPA configured to scale API replicas based on CPU utilization.
- Rolling deployment strategy used to avoid downtime while updating versions.
- Kubernetes self-healing ensures failed pods are recreated automatically.

These practices improve reliability while controlling cloud resource consumption.

---

# 10. Conclusion

The solution demonstrates a production-style Kubernetes deployment with:

- Containerization
- Service communication
- Persistent database
- Automated recovery
- Scaling
- Deployment management