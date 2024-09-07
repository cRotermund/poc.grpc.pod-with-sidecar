#Preserve to reset afterwards
$cur_dir = Get-Location
$script_dir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)

#Nav to location of script
cd $script_dir

#Exec
kubectl apply -f ../../deploy/app.deployment.yaml
# kubectl rollout restart deployment/worker-poc

#Return to original location
cd $cur_dir