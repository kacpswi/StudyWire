export interface User{
    id: number
    name: string;
    surename: string;
    token: string;
    schoolId: number|null;
    email: string;
    userRoles: string[];
    schoolName: string;
}