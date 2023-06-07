async function updateStatusComplaint(complaintId) {
    const res = await sendReqWithoutBody('get', `/Complaints/UpdateStatus?id=${complaintId}`);
    if (!res) return false;

    document.getElementById("btnComplaint" + complaintId).style.display = "none";
    document.getElementById("successComplaint" + complaintId).style.display = "block";
}

async function sendReqWithoutBody(method, url) {
    try {
        return await axios({
            method,
            url,
        });
    } catch (error) {
        console.error(error);
        return false;
    }
}
