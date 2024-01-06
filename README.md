Backend for a question-answering application using C#

** .Net Interview Question

Objective: Create the backend for a question-answering application using C# that
includes functionality for users to ask questions, provide answers, and optionally vote
on both questions and answers. Each question should have a tag so user can filter the
questions by the tags.
Note: It is not expect to have a full working application. This is only the backend API,
database and bulk import tool.
Requirements:
Database Design (Code-First Approach):
Design the database schema using Entity Framework (EF) Code First.
Define the following entities:
User,
Question,
Answer,
Question Tags - every question could have many tags
(Optional) Votes - store votes for both questions and answers
Implement Import Logic:
Develop bulk import logic to facilitate the addition of questions and answers.
You can either generate sample questions and answers programmatically or
provide an approach to import them from file.
Design API:
Develop a RESTful API to interact with the application.
Create endpoints for the following actions:
Posting new questions with specified tags (tags are free text, not predefined
list).
Posting answers to existing questions.
(Optional) Voting on questions and answers.
Querying questions by tags.
Retrieving question details along with answers.
Note: Ensure that the API is well-documented.
Deliverables:
Codebase with EF Code First database design.
Import logic for questions and answers.
RESTful API with endpoints for the specified actions.
