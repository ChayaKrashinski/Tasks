const userURL = 'user';
const todoURL = 'todo'

const token = localStorage.getItem('token');
const auth = `Bearer ${token}`

fetch(userURL, {
    method: 'GET',
    headers: {
        'Authorization': auth,
        'Accept': 'application/json',
    },
})
    .then(response => response.json())
    .then(data => {
        // if (data.ok)
        drawUser(data);
    })
    .catch(error => {
        console.error('error:', error)
        alert("unAuthorize!!")
        // location.href = "index.html"
    });

const drawUser = (user) => {
    const div = document.createElement('div');
    const h2 = document.createElement('h2');
    h2.innerHTML = `Hello ${user.userName} 🤝`;
    div.appendChild(h2);
    const userDetails = document.getElementById('userDetails');
    userDetails.appendChild(div);
}

const drawTable = () => {
    const tasks = document.getElementById('tasks');
    const table = document.createElement('table')
    table.id = 'tasksList'
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

const drawTask = (task) => {
    const tasksList = document.getElementById('tasksList');
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

     editButton.onclick = () => {
        if (editButton.innerHTML == 'edit') {
            editButton.innerHTML = 'ok'
            nameInput.disabled = false;
            isDoneInput.disabled = false;
        }
        else {
            const newTask = {
                "id": task.id,
                "name": nameInput.value.trim(),
                "isDone": isDoneInput.checked
            }
            fetch(`${todoURL}/UpdateTask/${task.id}`, {
                method: 'PUT',
                headers: {
                    'Authorization': auth,
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newTask)
            })
                .then(response => {
                    alert(response.status)
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
                tasksList.removeChild(tr)
                alert('the task deleted')
            })
            .catch(error => {
                console.error('error:', error)
                alert("error!!")
                // location.href = "login.js"
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


const tasksBtn = document.getElementById('tasksBtn')
tasksBtn.onclick = () => {
    tasksBtn.disabled = false
    fetch(`${todoURL}/tasksList`, {
        method: 'GET',
        headers: {
            'Authorization': auth,
            'Accept': 'application/json',
        },
    })
        .then(response => response.json())
        .then(data => {
            drawTable();
            data.forEach(task => {
                drawTask(task)
            });
        })
        .catch(error => {
            console.error('error:', error)
            alert("noData!!")
            // location.href = "login.js"
        });
}

