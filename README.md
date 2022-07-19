# AdvDiversityRepo

This repository consists of my contribution to the build for The Institute to Advance Diversity.

Front End - React.js, Back End - .NET (C#), Database - MS SQL

Features I worked on:

<strong>1. Mentor Dashboard </strong>

- The mentor dashboard help the current logged in mentor interact with their data in a singular, straightforward view. 
- It allows the mentor to see their profile information, calendar details, and mentor matches. 
- The name, description, appointments, and mentee information of the dashboard will be generated based on the configurations stored in the SQL database.
- React libraries: Bootstrap, toastr

<strong>2. Mentor Calendar </strong>

- A secure list of mentor's upcoming and past appointments that currently logged in mentor can use to interact with thier mentees.
- Mentor has the ability to comment, go to mentee profile, and cancel appointment if needed.
- Create Appointment utilizes calendly to provide mentor a link to create set timed appointments with mentee(s) in the future.
- React libraries: Calendly, react-router-dom, SimpleBar, Bootstrap, toastr

<strong>3. Mentor Matches </strong>

- Provide a secure list of mentees that are matched with currently logged in mentor.
- Mentor has ability to delete a specific mentee if no longer working with mentee.
- To get a video call with mentee, a zoom button is listed in the mentee card to send video link.
- After an appointment session with mentee, mentor can send survey to mentee to gather feedback.
- React libraries: Swal, FontAwesomeIcon, Bootstrap, toastr
