import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Point, Polygon } from '../models/polygon.model';
import { APP_CONFIG } from '../app.component';

@Injectable({
 providedIn: 'root'
})
export class RequestService {
 constructor(private http: HttpClient) { }

 checkPointInsidePolygon(polygonPoints: Point[], point: Point) {
    const body = { polygon: polygonPoints,  point: point};
    APP_CONFIG.API_URL
    return this.http.post(`${APP_CONFIG.API_URL}/Polygon/CheckPointInsidePolygon`, body);
 }

 savePolygon(polygonPoints: Point[], polygonName: string) {
  const body = { points: polygonPoints, name: polygonName };
  return this.http.post(`${APP_CONFIG.API_URL}/Polygon/SavePolygon`, body);
}

getPolygons(): Observable<Polygon[]> {
  return this.http.get<Polygon[]>(`${APP_CONFIG.API_URL}/Polygon/Polygons`);
}
}