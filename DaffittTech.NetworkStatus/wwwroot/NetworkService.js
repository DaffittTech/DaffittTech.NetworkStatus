window.networkStatus = {
    dotNetObject: null,
    initialize: function (dotNetObject) {
        // Register the DoNetObject to which this applies
        this.dotNetObject = dotNetObject;

        // Update the status immediately
        if (navigator.onLine) {
            this.fetchStatus();
        }
        else {
            this.notifyStatusChanged(false);
        }
    },
    fetchStatus: function () {
        const controller = new AbortController();
        const signal = controller.signal;
        const timeout = setTimeout(() => controller.abort(), 3000); // Timeout after 3 seconds

        // Ping a Google service to see if the Internet is active.
        fetch('https://www.google.com/generate_204', { method: 'HEAD', mode: 'no-cors', signal })
            .then(response => {
                clearTimeout(timeout);
                window.networkStatus.notifyStatusChanged(true);
            })
            .catch(error => {
                clearTimeout(timeout);
                window.networkStatus.notifyStatusChanged(false);
            });
    },
    async checkStatus() {
        const controller = new AbortController();
        const signal = controller.signal;
        const timeout = setTimeout(() => controller.abort(), 3000);
        if (navigator.onLine) {
            try {
                await fetch('https://www.google.com/generate_204', { method: 'HEAD', mode: 'no-cors', signal });
                clearTimeout(timeout);
                return true;
            } catch (error) {
                clearTimeout(timeout);
                return false;
            }
        } else {
            return false;
        }
    },
    notifyStatusChanged: function (status) {
        const message = status === true ? 'Network Status: Connected' : 'Network Status: Connection lost';
        console.log(message);
        if (this.dotNetObject) {
            this.dotNetObject.invokeMethodAsync("NotifyNetworkStatusChanged", status);
        }
    },
    monitorStatus: function (seconds) {
        this.intervalId = setInterval(this.fetchStatus, seconds * 1000);
    },
    dispose: function () {
        // Stop the setInterval operation
        clearInterval(this.intervalId);
    },
};
