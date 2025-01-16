window.networkStatus = {
    dotNetObject: null,
    initialize: function (dotNetObject) {
        // Register the DoNetObject to which this applies
        this.dotNetObject = dotNetObject;
    },
    fetchStatus: function () {
        const controller = new AbortController();
        const signal = controller.signal;
        const timeout = setTimeout(() => controller.abort(), 1000); // Timeout after 1 second

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
        try {
            await fetch('https://www.google.com/generate_204', { method: 'HEAD', mode: 'no-cors', signal });
            clearTimeout(timeout);
            return true;
        } catch (error) {
            clearTimeout(timeout);
            return false;
        }
    },
    monitorStatus: function (seconds) {
        if (!seconds || seconds <= 0) {
            clearInterval(this.intervalId);
        }
        else if (seconds > 0) {
            this.intervalId = setInterval(this.fetchStatus, seconds * 1000);
        }
    },
    notifyStatusChanged: function (status) {
        if (this.dotNetObject) {
            this.dotNetObject.invokeMethodAsync("NotifyNetworkStatusChanged", status);
        }
    },
    dispose: function () {
        // Stop the setInterval operation
        clearInterval(this.intervalId);
    },
};
