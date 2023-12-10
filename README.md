# Welcome to Cint's Coding Challenge!


> Disclaimer: this is a technical coding test and its only purpose is to demonstrate software development skills. It is not something Cint can/will profit from in any way.
>
> You can spend as much or as little time as you like, but typically 3 or 4 hours should be enough.

## Introduction

You have just started your first day of work and your task is to continue implementing a feature that another developer has left incomplete.

The project is a website that will allow users to search through all the surveys in the system by name and display them in a particular order.

The feature being built needs to take the data returned from the back-end API endpoint and display it in the front-end website.

You can use whatever frameworks, packages, and components you like.

## Project requirements
The project requires the following:
- .NET 6 SDK and runtime
- NPM package manager


## API endpoints
The API has the following endpoints:
- GET `/survey/search`: search for a survey by name
- GET `/survey/<id>`: get a survey by ID
- POST `/survey/`: create a new survey


## Website
Currently, the website displays the data returned from the search API endpoint.

The website uses a basic WebPack setup to build the assets for the front-end. The source files can be found in `<project_root>/app/src`.

To build the front-end assets, simpy run `npm run build` from the front-end source directory. Alternatively run the `.\build.ps1` powershell script from the repository root.


# Task requirements

In addition to the below, please ensure:
- the solution builds successfully
- all TODOs have been implemented
- all tests pass


## Back-end task
See <a href="README_back-end.md">Back-end tasks</a> README file.

## Front-end task
See <a href="README_front-end.md">Front-end tasks</a> README file.

# Notes
Here are some things to remember when completing the coding challenge:
1. You have up to a week to complete the challenge but we understand life can change suddenly so reach out if you need more time.
1. Make sure you write the code as if you were releasing to a production environment (i.e. clean, maintainable and correct).
1. You are encouraged to review <a href="README_marking-criteria.md">Cint's marking criteria</a> for reviewing the coding challenge.
1. We welcome your feedback! If anything is unclear or you have some questions or comments, just let us know.
