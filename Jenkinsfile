pipeline {
    agent { docker { image 'mcr.microsoft.com/dotnet/core/sdk:2.2-alpine' } }
    environment {HOME = '/tmp'} 
    stages {
    stage('Git') {
      // Get some code from a GitHub repository
      steps{
          git 'https://github.com/orontr/Project_Management.git'
      }
   }
    stage('Dotnet Restore'){
        steps{
        bat "dotnet restore"
        }
    }
    
   stage('Build'){
          steps{
               bat "dotnet build"
               }
    }
    stage('Run Tests'){
          steps{
               bat "dotnet test"
          }
    }
}
}
