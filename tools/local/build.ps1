#Preserve to reset afterwards
$cur_dir = Get-Location
$script_dir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)

#Nav to location of script
cd $script_dir

#Exec docker builds
docker build ../../src/Runtime.Agent -t runtime-poc-agent:latest
docker build ../../src/Runtime.Worker -t runtime-poc-worker:latest

#Return to original location
cd $cur_dir