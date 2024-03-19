import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
 providedIn: 'root'
})
export class RequestService {
 constructor(private http: HttpClient) { }

 savePolygon(polygonPoints: { x: number; y: number }[], point: {x:number,y:number}) {
    const body = { polygon: polygonPoints,  point: point};
    return this.http.post('http://your-server-url/api/polygons', body);
 }
}