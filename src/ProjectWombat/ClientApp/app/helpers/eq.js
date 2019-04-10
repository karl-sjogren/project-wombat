import { helper } from '@ember/component/helper';

export function eq(params) {
  if(params.length !== 2) {
    console.warn('Invalid number or arguments passed to eq helper. Defaulting to false.');
    return false;
  }
  
  return params[0] === params[1];
}

export default helper(eq);
