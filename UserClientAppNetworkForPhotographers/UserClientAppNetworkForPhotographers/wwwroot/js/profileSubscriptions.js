let photographerIdForSubscription;

let countSubscribers;
let countSubscriptions;


const btnSubscribe = document.getElementById("btnSubscribe");
const btnUnsubscribe = document.getElementById("btnUnsubscribe");

const divCountSubscribers = document.getElementById("divCountSubscribers");
const divCountSubscriptions = document.getElementById("divCountSubscriptions");


function initSubscriptions(argPhotographerId) {
    photographerIdForSubscription = argPhotographerId;

    checkSubscription();
    getCountSubscribers();
    getCountSubscriptions();
}


async function checkSubscription() {
    const res = await serverCheckSubscription(photographerIdForSubscription);
    if (!res) return false;

    if (res.data) {
        btnSubscribe.style.display = "none";
        btnUnsubscribe.style.display = "block";
    }
}

async function getCountSubscribers() {
    const res = await serverGetCountSubscribers(photographerIdForSubscription);
    if (!res) return false;

    countSubscribers = res.data;
    divCountSubscribers.innerHTML = countSubscribers;
}

async function getCountSubscriptions() {
    const res = await serverGetCountSubscriptions(photographerIdForSubscription);
    if (!res) return false;

    countSubscriptions = res.data;
    divCountSubscriptions.innerHTML = countSubscriptions;
}



async function createSubscription() {
    const res = await serverCreateSubscription(photographerIdForSubscription);
    if (!res) return false;

    countSubscribers++;
    divCountSubscribers.innerHTML = countSubscribers;

    btnSubscribe.style.display = "none";
    btnUnsubscribe.style.display = "block";
}

async function deleteSubscription() {
    const res = await serverDeleteSubscription(photographerIdForSubscription);
    if (!res) return false;

    countSubscribers--;
    divCountSubscribers.innerHTML = countSubscribers;

    btnSubscribe.style.display = "block";
    btnUnsubscribe.style.display = "none";
}




// Modal window for subscribers

const modalAllSubscribers = document.getElementById('modalAllSubscribers');
modalAllSubscribers.addEventListener('show.bs.modal', async function (event) {
    const res = await serverGetSubscribers(photographerIdForSubscription);
    if (!res) return false;

    const photographers = res.data;

    const parent = document.getElementById("modalAllSubscribersBody");
    addPhotographersForModal(photographers, parent);


    const hModal = document.querySelector("#modalAllSubscribersHeader>h5");
    const count = photographers.length;
    const form = getFormSubscriber(count);
    hModal.innerHTML = `${count} ${form}`;
});


// Modal window for subscriptions

const modalAllSubscriptions = document.getElementById('modalAllSubscriptions');
modalAllSubscriptions.addEventListener('show.bs.modal', async function (event) {
    const res = await serverGetSubscriptions(photographerIdForSubscription);
    if (!res) return false;

    const photographers = res.data;

    const parent = document.getElementById("modalAllSubscriptionsBody");
    addPhotographersForModal(photographers, parent);


    const hModal = document.querySelector("#modalAllSubscriptionsHeader>h5");
    const count = photographers.length;
    const form = getFormSubscriprion(count);
    hModal.innerHTML = `${count} ${form}`;
});