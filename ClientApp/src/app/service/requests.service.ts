import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Point, Polygon } from '../models/polygon.model';

@Injectable({
 providedIn: 'root'
})
export class RequestService {
 constructor(private http: HttpClient) { }

 checkPointInsidePolygon(polygonPoints: Point[], point: Point) {
    const body = { polygon: polygonPoints,  point: point};
    return this.http.post('https://localhost:7124/Polygon/CheckPointInsidePolygon', body);
 }

 savePolygon(polygonPoints: Point[], polygonName: string) {
  const body = { points: polygonPoints, name: polygonName };
  return this.http.post('https://localhost:7124/Polygon/SavePolygon', body);
}

getPolygons(): Observable<Polygon[]> {
  return this.http.get<Polygon[]>('https://localhost:7124/Polygon/Polygons');
}
}