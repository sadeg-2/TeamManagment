# TeamManagment

## Description

**TeamManagment** is a web-based application designed to streamline team collaboration and task management within organizations. The platform enables users to manage personal and group tasks, communicate instantly, and monitor team activity with a modern, user-friendly interface. This project is intended for small to medium-sized teams, managers, and anyone seeking to organize and coordinate collaborative work.

**Main Goals:**
- Simplify team collaboration and task assignment.
- Enhance communication through integrated chat features.
- Provide user-friendly interfaces for managing tasks, profiles, and notifications.

## Features & Technology

### Key Features
- **Task Management:** Create, assign, and track personal and group tasks.
- **Real-Time Chat:** Communicate instantly with team members using SignalR-based messaging.
- **Notification System:** Receive timely updates and reminders for tasks and team activities.
- **Profile Page:** Each user has a dedicated profile page displaying their information, number of friends, and activity metrics.
- **Friends System:** Add, remove, and view friends; see their status and interact with them.
- **User Registration & Authentication:** Secure sign-up and login flows.
- **Form Validation:** Robust input validation for task and user forms.
- **Responsive UI:** Modern, mobile-friendly design using Bootstrap and Metronic themes.

### Technologies & Libraries
- **ASP.NET Core MVC** for backend logic, controllers, and views.
- **SignalR** for real-time chat and notifications.
- **Bootstrap & Metronic** for UI layout, components, and styling.
- **jQuery & jQuery Validation** for DOM manipulation and form validation.
- **Toastify** for notification messages.
- **Flatpickr & Select2** for advanced form controls.

## Usage

### User Registration & Login
- **Sign Up:** New users can register accounts via the registration page.
- **Authentication:** Secure login ensures only authorized access to personal and team data.

### Profile Management
- **Profile Page:** View and edit your personal information.
    - See your number of friends and connection status.
    - View your activity metrics and recent interactions.
- **Friends List:** Add or remove friends, view their profiles, and see their online status.

### Task Management
- **Create Tasks:** Add personal or team tasks using the dashboard.
- **Assign Tasks:** Assign tasks to yourself or other team members.
- **Track Progress:** Monitor completion status and deadlines.
- **Form Validation:** All forms include robust validation to ensure accurate data entry.

### Messaging & Notifications
- **Chat Feature:** Use the integrated chat to communicate instantly with team members.
    - Send direct or group messages.
    - Receive messages in real time.
- **Notifications:** Get updates on task assignments, friend requests, and activity.

**Sample Chat Usage (JavaScript Console):**
```javascript
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start().then(function () {
    // Ready to send messages
    connection.invoke("SendMessage", "Username", "Hello Team!");
});
```

### Dashboard & UI
- **Task Dashboard:** Overview of tasks, their status, and assignments.
- **Friend Activity:** See updates from friends and team members.
- **Modern UI:** Responsive design adapts to desktop and mobile devices.

## Contributing

We welcome contributions to TeamManagment!

1. **Fork the repository** on GitHub.
2. **Clone your fork** and create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes** and commit them.
4. **Push your branch** to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```
5. **Open a pull request** describing your changes.

Please ensure your code follows project conventions and includes appropriate tests or documentation.

## License

This project uses the MIT License.  
Third-party libraries such as Bootstrap and jQuery also use the MIT License.  
See the respective `LICENSE` files for details.

## Contact / Author Info

**Author:** [sadeg-2](https://github.com/sadeg-2)  
For questions or suggestions, please contact via GitHub.
