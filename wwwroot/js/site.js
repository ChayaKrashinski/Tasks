const userURL = 'user';
const todoURL = 'todo'

const unAuth = () => {
    alert("unauthorize")
        // location.href = "index.html"
}

const token = localStorage.getItem('token');
const auth = `Bearer ${token}`

fetch(userURL, {
        method: 'GET',
        headers: {
            'Authorization': auth,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
    .then(response => response.json())
    .then(data => {
        if (data.id == undefined) throw new Error(`data status is: ${data.status}`);
        helloUser(data);
        localStorage.setItem("isAdmin", data.isAdmin)

    })
    .catch((error) => {
        if (error.status == 401) unAuth()
        else
            console.error(error);
    })

const adminOption = document.getElementsByClassName('adminOption')
if (localStorage.getItem('isAdmin') == false)
    adminOption.style = "display:none"


const helloUser = (user) => {
    const div = document.createElement('div');
    const h2 = document.createElement('h2');
    h2.innerHTML = `Hello ${ user.userName }🤝`;
    div.appendChild(h2);
    const userDetails = document.getElementById('userDetails');
    userDetails.appendChild(div);
}


///&&&///

const drawUser = (user, tableId) => {
    const userTable = document.getElementById(tableId);
    const tr = document.createElement('tr')


    const edit = document.createElement('th')
    const editButton = document.createElement('button')
    editButton.innerHTML = 'edit'

    const id = document.createElement('th')
    id.innerHTML = user.id;

    const password = document.createElement('th')
    const passwordInput = document.createElement('input')
    passwordInput.type = Text;
    passwordInput.value = user.password;

    const name = document.createElement('th')
    const nameInput = document.createElement('input')
    nameInput.type = Text;
    nameInput.value = user.userName

    const Delete = document.createElement('th')
    const deleteButton = document.createElement('button')
    deleteButton.innerHTML = 'delete'
    if (tableId != 'usersList') {
        deleteButton.disabled = true;
        deleteButton.innerHTML = '-'
    }

    nameInput.disabled = true;
    passwordInput.disabled = true;

    if (user.id == -1) {
        editButton.innerHTML = 'add'
        id = '-'
    }

    const addUser = () => {
        const newUser = {
            "password": passwordInput.value.trim(),
            "isAdmin": user.isAdmin,
            "id": user.id,
            "userName": nameInput.value.trim(),
            "tasksList": user.tasksList
        }
        fetch(`${ userURL }/UpdateUser/${ user.id }`, {
                method: 'POST',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newUser)
            })
            .then(response => {
                if (response.status == 401) unAuth();
                editButton.innerHTML = 'edit';
                nameInput.disabled = true;
                passwordInput.disabled = true;
            })
            .then((data) => {
                id.value = data.id
                drawUser({ password: "-1", isAdmin: false, id: -1, userName: "", tasksList: [] })
            })
            .catch(error => {
                console.error('error:', error)
            });
    }

    const updateUser = () => {

        const newUser = {
            password: passwordInput.value.trim(),
            isAdmin: user.isAdmin,
            id: user.id,
            userName: nameInput.value.trim(),
            tasksList: user.tasksList
        }
        fetch(`${userURL}/UpdateUser/${user.id}
                `, {
                method: 'PUT',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newUser)
            })
            .then(response => {
                if (response.status == 401) unAuth()
                editButton.innerHTML = 'edit'
                nameInput.disabled = true;
                passwordInput.disabled = true;
            })
            .catch(error => {
                console.error('error:', error)
            });
    }

    editButton.onclick = () => {
        if (editButton.innerHTML == 'ok') updateUser()
        else if (editButton.innerHTML == 'edit') {
            editButton.innerHTML = 'ok'
            nameInput.disabled = false;
            passwordInput.disabled = false;
        } else if (editButton.innerHTML == 'add') addUser()
    }

    deleteButton.onclick = () => {
        fetch(`${userURL}/DeleteUser/${user.id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            })
            .then(response => {
                if (response.status == 401) unAuth();
                response.json();
            })
            .then((usersList) => {
                usersList.removeChild(tr);
                alert('the user deleted');
            })
            .catch(error => {
                console.error('error:', error)
                alert("only admin can remove user, you dont admin!!")
            });

    }

    edit.appendChild(editButton)
    name.appendChild(nameInput)
    password.appendChild(passwordInput)
    Delete.appendChild(deleteButton)

    tr.appendChild(edit)
    tr.appendChild(id)
    tr.appendChild(password)
    tr.appendChild(name)
    tr.appendChild(Delete)

    userTable.appendChild(tr)
}

const drawUsersTable = (id) => {
    const users = document.getElementById('users');
    const table = document.createElement('table')
    table.id = id
    const tr = document.createElement('tr')


    const th1 = document.createElement('th')
    th1.innerHTML = 'Edit'
    const th2 = document.createElement('th')
    th2.innerHTML = 'Id'
    const th3 = document.createElement('th')
    th3.innerHTML = 'Password'
    const th4 = document.createElement('th')
    th4.innerHTML = 'Name'
    const th5 = document.createElement('th')
    th5.innerHTML = 'Delete'

    const chn = [th1, th2, th3, th4, th5]
    chn.forEach(ch => tr.appendChild(ch))
    const tbody = document.createElement('tbody')
    table.appendChild(tr)
    table.appendChild(tbody)
    users.appendChild(table)
}

const drawTasksTable = (id) => {
    let tasks = document.getElementById('userTasks');

    if (id != 'myTasks')
        tasks = document.getElementById('usersTasks');

    const table = document.createElement('table')
    table.id = id
    const tr = document.createElement('tr')

    const th1 = document.createElement('th')
    th1.innerHTML = 'Edit'
    const th2 = document.createElement('th')
    th2.innerHTML = 'Id'
    const th3 = document.createElement('th')
    th3.innerHTML = 'Name'
    const th4 = document.createElement('th')
    th4.innerHTML = 'Is Done'
    const th5 = document.createElement('th')
    th5.innerHTML = 'Delete'

    const chn = [th1, th2, th3, th4, th5]
    chn.forEach(ch => tr.appendChild(ch))
    const tbody = document.createElement('tbody')
    table.appendChild(tr)
    table.appendChild(tbody)
    tasks.appendChild(table)
}

const drawTask = (task, tableId) => {
    const tasksList = document.getElementById(tableId);
    const tr = document.createElement('tr')

    const edit = document.createElement('th')
    const editButton = document.createElement('button')
    editButton.innerHTML = 'edit'

    const id = document.createElement('th')
    id.innerHTML = task.id;

    const name = document.createElement('th')
    const nameInput = document.createElement('input')
    nameInput.type = Text;
    nameInput.value = task.name

    const isDone = document.createElement('th')
    const isDoneInput = document.createElement('input')
    isDoneInput.type = 'checkbox';
    isDoneInput.checked = task.isDone

    const Delete = document.createElement('th')
    const deleteButton = document.createElement('button')
    deleteButton.innerHTML = 'delete'

    nameInput.disabled = true;
    isDoneInput.disabled = true;

    if (task.id == -1) {
        editButton.innerHTML = 'new';
        id.innerHTML = '-';
        deleteButton.disabled = true;
    }

    const updateTask = () => {
        const newTask = {
            "id": task.id,
            "name": nameInput.value.trim(),
            "isDone": isDoneInput.checked
        }
        fetch(`${ todoURL }/UpdateTask/${ task.id }`, {
                method: 'PUT',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newTask)
            })
            .then(response => {
                if (response.status == 401) unAuth()
                editButton.innerHTML = 'edit'
                nameInput.disabled = true;
                isDoneInput.disabled = true;
            })
            .catch(error => {
                console.error('error:', error)
                alert("noData!!")
                    // location.href = "login.js"
            });
    }

    const addTask = () => {

        const newTask = {
            "id": task.id,
            "name": nameInput.value.trim(),
            "isDone": isDoneInput.checked
        }
        fetch(`${todoURL}/AddTask`, {
                method: 'POST',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newTask)
            })
            .then(response => response.json())
            .then(response => {
                if (response.status == 401) unAuth();
                else {
                    editButton.innerHTML = 'edit'
                    nameInput.disabled = true;
                    isDoneInput.disabled = true;
                    deleteButton.disabled = false;
                    id.innerHTML = response
                    drawTask({ id: -1, name: "", isDone: false }, tableId)
                }

            })
            .catch(error => {
                console.error('error:', error)
            });
    }



    editButton.onclick = () => {
        if (editButton.innerHTML == 'add') addTask()
        else if (editButton.innerHTML == 'new') {
            editButton.innerHTML = 'add'
            nameInput.disabled = false;
            isDoneInput.disabled = false;
        } else if (editButton.innerHTML == 'edit') {
            editButton.innerHTML = 'ok'
            nameInput.disabled = false;
            isDoneInput.disabled = false;
        } else updateTask();
    }

    deleteButton.onclick = () => {
        fetch(`${todoURL}/DeleteTask/${task.id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            })
            .then(response => {
                if (response.status == 401) unAuth()
                tasksList.removeChild(tr)
                alert('the task deleted')
            })
            .catch(error => {
                console.error('error:', error)
            });

    }

    edit.appendChild(editButton)
    name.appendChild(nameInput)
    isDone.appendChild(isDoneInput)
    Delete.appendChild(deleteButton)

    tr.appendChild(edit)
    tr.appendChild(id)
    tr.appendChild(name)
    tr.appendChild(isDone)
    tr.appendChild(Delete)

    tasksList.appendChild(tr)
}

//tasks
/////////////////////////////////////////////////////
const tasksBtn = document.getElementById('tasksBtn')
tasksBtn.onclick = () => {
    tasksBtn.style = "display:none"
    fetch(`${todoURL}/tasksList`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'content-type': 'application/json',
                'Authorization': auth
            },
        })
        .then(response => response.json())
        .then(data => {
            drawTasksTable('myTasks');
            data.forEach(task => {
                drawTask(task, 'myTasks')
            });
            drawTask({ id: -1, name: "", isDone: false }, 'myTasks');

        })
        .catch(error => {
            console.error('error:', error)
        });
}

const tasksUsersBtn = document.getElementById('tasksUsersBtn')
tasksUsersBtn.onclick = () => {
    tasksUsersBtn.style = "display:none"
    fetch(`${todoURL}/allTasksList`, {
            method: 'GET',
            headers: {
                'Authorization': auth,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(data => {
            if (data) {
                drawTasksTable('allTasks');
                data.forEach(task => {
                    drawTask(task, 'allTasks')
                });
                drawTask({ id: -1, name: "", isDone: false }, 'allTasks')
            }
        })
        .catch(error => {
            console.error('error:', error)
        });
}



//users
/////////////////////////////////////////////////////

const usersBtn = document.getElementById('usersBtn')
usersBtn.onclick = () => {

    usersBtn.style = "display:none"
    fetch(`${userURL}/GetAllUsers`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'content-type': 'application/json',
                'Authorization': auth
            },
        })
        .then(response => response.json())
        .then(data => {
            drawUsersTable('usersList');
            data.forEach(user => {
                drawUser(user, 'usersList')
            });
            drawUser({ password: "-1", isAdmin: false, id: -1, userName: "", tasksList: [] }, 'usersList');
        })
        .catch(error => {});

}


const userBtn = document.getElementById('userBtn')
userBtn.onclick = () => {
    userBtn.style = "display:none"
    fetch(userURL, {
            method: 'GET',
            headers: {
                'Authorization': auth,
                'Accept': 'application/json',
            },
        })
        .then(response => response.json())
        .then(data => {
            drawUsersTable('userTable');
            drawUser(data, 'userTable')
        })
        .catch(error => {
            console.error(error);
        });

}