const uri = 'user';

const token = localStorage.getItem('token');
const auth = `Bearer ${token}`

const drawUser = (user) => {
    const div = document.createElement('div');
    const h2 = document.createElement('h2');
    h2.innerHTML = `Hello ${user.userName} 🤝`;
    div.appendChild(h2);
    const userDetails = document.getElementById('userDetails');
    userDetails.appendChild(div);
}

const drawTask = (task) => {
    const tasksTable = document.getElementById('tasksTable');
    const tr = document.createElement('tr')

    const th1 = document.createElement('th')
    const editButton = document.createElement('button')
    editButton.innerHTML = 'edit'

    const th2 = document.createElement('th')
    th2.innerHTML = task.id;

    const th3 = document.createElement('th')
    const input3 = document.createElement('input')
    input3.type = Text;
    input3.value = task.name

    const th4 = document.createElement('th')
    const input4 = document.createElement('input')
    input4.type = Text;
    input4.value = task.isDone

    const th5 = document.createElement('th')
    const deleteButton = document.createElement('button')
    deleteButton.innerHTML = 'delete'

    input3.disabled = true;
    input4.disabled = true;

    editButton.onclick = () => {
        if (editButton.innerHTML == 'edit') {
            editButton.innerHTML = 'ok'
            input3.disabled = false;
            input4.disabled = false;
        }
        else {
            editButton.innerHTML = 'edit'
            input3.disabled = true;
            input4.disabled = true;
        }
    }

    deleteButton.onclick = () => {
        tasksTable.removeChild(tr)
    }

    th1.appendChild(editButton)
    th3.appendChild(input3)
    th4.appendChild(input4)
    th5.appendChild(deleteButton)

    tr.appendChild(th1)
    tr.appendChild(th2)
    tr.appendChild(th3)
    tr.appendChild(th4)
    tr.appendChild(th5)

    tasksTable.appendChild(tr)
}


fetch(uri, {
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
        drawTask(data.tasksList[0])
        drawTask(data.tasksList[1])
        // else console.error('the data is not ok');
    })
    .catch(error => {
        console.error('error:', error)
        alert("unAuthorize!!")
        // location.href = "index.html"
    });