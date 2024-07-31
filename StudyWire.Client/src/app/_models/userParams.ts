export class UserParams{
    searchPhrase: string = "";
    pageNumber = 1;
    pageSize = 20;
    sortBy: string = "";
    sortDirection: string = "";

    constructor(serachPhrase:string, sortBy:string, sortDirection:string) {
        this.searchPhrase = serachPhrase;
        this.sortBy = sortBy;
        this.sortDirection = sortDirection;
    }
}