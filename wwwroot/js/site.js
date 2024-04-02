const uri = '/user';

const token = localStorage.getItem('token');
const auth = { 'Authorization': `Bearer ${token}` }
alert("scazg");
fetch(uri, {
        method: 'GET',
        headers: {
            auth,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
    .then(response => response.json())
    .then(data => {
        localStorage.setItem("user", data)
        location.href = "index.html"
    })
    .catch(error => {
        console.error('error:', error)
        location.href = "index.html"
    });


// function getItems() {
//     fetch(uri)
//         .then(response => response.json())
//         .then(data => _displayItems(data))
//         .catch(error => console.error('Unable to get items.', error));
// }

// function addItem() {
//     const addNameTextbox = document.getElementById('add-name');

//     const item = {
//         IsDo: false,
//         name: addNameTextbox.value.trim()
//     };

//     fetch(uri, {
//             method: 'POST',
//             headers: {
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json'
//             },
//             body: JSON.stringify(item)
//         })
//         .then(response => response.json())
//         .then(() => {
//             getItems();
//             addNameTextbox.value = '';
//         })
//         .catch(error => console.error('Unable to add item.', error));
// }

// function deleteItem(id) {
//     fetch(`${uri}/${id}`, {
//             method: 'DELETE'
//         })
//         .then(() => getItems())
//         .catch(error => console.error('Unable to delete item.', error));
// }

// function displayEditForm(id) {
//     const item = pizzas.find(item => item.id === id);

//     document.getElementById('edit-name').value = item.name;
//     document.getElementById('edit-id').value = item.id;
//     document.getElementById('edit-IsDo').checked = item.IsDo;
//     document.getElementById('editForm').style.display = 'block';
// }

// function updateItem() {
//     const itemId = document.getElementById('edit-id').value;
//     const item = {
//         id: parseInt(itemId, 10),
//         IsDo: document.getElementById('edit-IsDo').checked,
//         name: document.getElementById('edit-name').value.trim()
//     };

//     fetch(`${uri}/${itemId}`, {
//             method: 'PUT',
//             headers: {
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json'
//             },
//             body: JSON.stringify(item)
//         })
//         .then(() => getItems())
//         .catch(error => console.error('Unable to update item.', error));

//     closeInput();

//     return false;
// }

// function closeInput() {
//     document.getElementById('editForm').style.display = 'none';
// }

// function _displayCount(itemCount) {
//     const name = (itemCount === 1) ? 'task' : 'task kinds';

//     document.getElementById('counter').innerText = `${itemCount} ${name}`;
// }

// function _displayItems(data) {
//     const tBody = document.getElementById('pizzas');
//     tBody.innerHTML = '';

//     _displayCount(data.length);

//     const button = document.createElement('button');

//     data.forEach(item => {
//         console.log(item);
//         let IsDoCheckbox = document.createElement('input');
//         IsDoCheckbox.type = 'checkbox';
//         IsDoCheckbox.disabled = true;
//         IsDoCheckbox.checked = item.isDo;

//         let editButton = button.cloneNode(false);
//         editButton.innerText = 'Edit';
//         editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

//         let deleteButton = button.cloneNode(false);
//         deleteButton.innerText = 'Delete';
//         deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

//         let tr = tBody.insertRow();

//         let td1 = tr.insertCell(0);
//         td1.appendChild(IsDoCheckbox);

//         let td2 = tr.insertCell(1);
//         let textNode = document.createTextNode(item.name);
//         td2.appendChild(textNode);

//         let td3 = tr.insertCell(2);
//         td3.appendChild(editButton);

//         let td4 = tr.insertCell(3);
//         td4.appendChild(deleteButton);
//     });

//     pizzas = data;
// }