# poc.grpc.pod-with-sidecar
Proof of concept project to prove out and illustrate grpc communication between a main application container and a sidecar container managed within a kubernetes pod.

# The Runtime.Agent
In the existing framework, our runtime does a number of things.  The key responsibilities I need to try and replicate 
in the multi-container environment are:

* Dispatching work items and handling work item results.
* Restarting the worker process (in this case a container) in the event of a crash -- does this need to happen in k8s?