import { v4 as uuidv4 } from 'uuid';

export function generateRandmString(length: number): string {
  return uuidv4().replace(/-/g, '').substring(0, length);
}

export function getRandomInt(min: number, max: number): number {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}
