export class BusyTracker {
    private pendingPromises = 0;

    track(promise: PromiseLike<any>) {
        this.pendingPromises++;
        promise.then(() => this.pendingPromises--, () => this.pendingPromises --);
    }

    get isBusy() {
        return this.pendingPromises > 0;
    }
}