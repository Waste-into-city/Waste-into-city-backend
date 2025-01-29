# Info

Database schema ( [DatabaseSchema.pdf](https://github.com/user-attachments/files/18589026/DatabaseSchema.pdf) ):

![DataSchema](https://github.com/user-attachments/assets/c9497299-dc65-49f6-9093-aea9c69760d9)

The application is designed to assist utilities in garbage collection. The application is designed to accomplish tasks in the following two areas:
- Garbage collection by users at specified points of interest. The user, having found garbage in the city, can add a point of interest on the map, attaching a photo and indicating how much work needs to be done to clean it up (difficulty level easy = 1-2 people, medium = 3-5 people, difficult = 5-10 people, very difficult = >10 people). The point of interest then appears on the map. Other users see this task on the map, it will have a status of pending validation. A task with pending status can be moved to active task status after the task has been checked by an administrator. Users can take on an active task if the number of people who have confirmed that they will also participate in the cleanup is not sufficient for the given difficulty level. By confirming the task, the user confirms that the garbage will be cleaned up within 24 hours. Before the task deadline, it is possible to cancel the task. After the task is completed, one of its participants confirms the end of the cleaning and attaches a photo of the cleaned area. The completed task disappears from the map and the information about it is specified in the task archive. The user can go to the tab of the tasks he/she took, can view information about the task and rate his/her colleagues who cleaned the garbage. Any user can send a complaint about a task within 12 hours after the end of its fulfillment, attaching a photo, thus proving that the task was not fulfilled. Complaints are reviewed by an administrator. If the complaint is confirmed by him, each participant loses a rating. Otherwise, the rating is lowered to the participant who sent the complaint. For the completed task after 12 hours, if there are no complaints accepted by the administrator, participants receive a rating, the number of points of which is determined by the level of evaluation of other colleagues for the work done.
- Users' evaluation of the filling of garbage cans. The application is also used not only to solve problems with scattered garbage, but also to inform utilities on how to optimize the routes of garbage trucks. The app also allows you to track the performance of your employees. In addition to points of interest on the map, dumpster locations are indicated on the map. Only administrator can indicate places with garbage cans on the map. Any user with a positive rating level can specify the filling status of garbage cans. In doing so, he/she can specify the level of fullness of bins of each type of garbage. If a user's rating is quite often different from the others, he loses rating points. He can raise his rating by removing trash in the points of interest. The administrator will have the opportunity to see the list of the most filled trash cans. In addition, if we have time, we can try to add a filtering function not only for the list of bins for the administrator, but also the ability to filter points of interest and garbage cans on the map.
