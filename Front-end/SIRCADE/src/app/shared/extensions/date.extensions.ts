export function getLocalDate(data: Date): Date {
  const date = new Date(data);
  const offset = date.getTimezoneOffset();
  date.setMinutes(date.getMinutes() - offset);
  return date;
}
