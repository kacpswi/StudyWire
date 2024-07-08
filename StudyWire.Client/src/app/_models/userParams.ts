export class UserParams{
    searchPhrase: string;
    pageNumber = 1;
    pageSize = 20;
    sortBy: string;
    sortDirection: string;

    constructor(serachPhrase:string|null, sortBy:string|null, sortDirection:string|null) {
        if (serachPhrase == null){
            this.searchPhrase = ""
        }
        else{
            this.searchPhrase = serachPhrase;
        } 


        if (sortBy == null){
            this.sortBy = ""
        }
        else{
            this.sortBy = sortBy;
        }

        if (sortDirection == null)
        {
            this.sortDirection = "DESC"
        }
        else{
            this.sortDirection = sortDirection;
        }
        
    }
}