import { helper } from '@ember/component/helper';

export function and(params) {
  if(params.length !== 2) {
    console.warn('Invalid number or arguments passed to and helper. Defaulting to false.');
    return false;
  }

  return !!params[0] && !!params[1];
}

export default helper(and);
