import * as platform from 'platform';

export class PlatformService {
    browser(userAgentString: string | undefined) {
        return this.parse(userAgentString).name;
    }
    device(userAgentString: string | undefined) {
        return this.parse(userAgentString).product;
    }
    os(userAgentString: string | undefined) {
        return this.parse(userAgentString).os;
    }
    private parse(userAgentString: string | undefined): any {
        return platform.parse
            ? platform.parse(userAgentString || '')
            : {};
    }
}