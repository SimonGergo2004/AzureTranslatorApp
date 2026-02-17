# Azure Translator MVC Application

This is a C# ASP.NET Core MVC-based web application that leverages the **Azure AI Translator** service to translate text across various languages.

## Features
- Text translation using Azure Cloud Services.
- Automatic source language detection (optional).
- Clean and responsive MVC user interface.

## SETUP AND CONFIGURATION

To run this application, you will need an active Azure subscription and a Translator resource.

### 1. Clone the Repository
First, clone the project to your local machine:
git clone https://github.com/SimonGergo2004/AzureTranslatorApp.git
cd AzureTranslatorApp

### 2. Azure Resource Setup
1. Log in to the Azure Portal (https://portal.azure.com/).
2. Create a Translator resource.
3. Once deployed, navigate to the Keys and Endpoint section.
4. Copy your Key 1 and the Location/Region (e.g., westeurope).

### 3. Configuration (Providing your Keys)
For security reasons, the appsettings.json file in this repository contains only placeholders. You can provide your own keys using one of the following methods:

#### Method A: appsettings.json (Quick Start)
Update the TranslatorApp/appsettings.json file:
{
  "AzureTranslator": {
    "Key": "YOUR_TRANSLATOR_KEY",
    "Endpoint": "https://api.cognitive.microsofttranslator.com/",
    "Region": "YOUR_REGION"
  }
}

Note: Do not commit this file back to a public repository if it contains your live keys!

#### Method B: User Secrets (Recommended for Security)
In Visual Studio, right-click on the project and select Manage User Secrets. Add the following JSON structure:
{
  "AzureTranslator": {
    "Key": "YOUR_ACTUAL_KEY",
    "Endpoint": "https://api.cognitive.microsofttranslator.com/",
    "Region": "YOUR_ACTUAL_REGION"
  }
}

## USAGE
1. Open the solution in Visual Studio and press F5 to run.
2. Enter the text you wish to translate on the home page.
3. Select your target language.
4. Click the Translate button.
5. The result will be displayed on the screen based on the Azure API response.

## TECH STACK
- Language: C# 12 / .NET 8
- Framework: ASP.NET Core MVC
- API: Azure AI Translator (REST)
