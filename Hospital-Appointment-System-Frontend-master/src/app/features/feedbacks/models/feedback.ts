export interface Feedback{
    id:number;
    userID:string;
    userFirstName:string;
    userLastName:string;
    text:string;
    createdDate:Date;
    deletedDate:Date;
    isApproved:boolean;
}