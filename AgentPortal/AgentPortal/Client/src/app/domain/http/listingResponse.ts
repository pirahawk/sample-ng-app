import { Link } from './link';

export class ListingResponse{
    public id: string;
    public numberBedrooms:number
    public postCode:string;
    public address:string;
    public description: string;
    public askingPrice:string;
    public expired:boolean;
    public links:Link[];
}